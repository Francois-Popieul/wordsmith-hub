import { DataTable } from "primereact/datatable";
import { DeleteIcon, PencilIcon } from "../../assets/icons/icons";
import Button from "../../components/ui/Button";
import { type schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";
import { Column } from "primereact/column";
import { usePricingUnits } from "../../hooks/useStaticData";

interface RateProps {
    rates: zod.infer<typeof schemas.RateDto>[];
    directCustomerCurrencySign: string;
    services: zod.infer<typeof schemas.Service>[];
    languages: zod.infer<typeof schemas.TranslationLanguage>[];
    onEdit: (id: string) => void;
    onDelete: (id: string) => void;
}

function RateDataTable({ rates, directCustomerCurrencySign, services, languages, onEdit, onDelete }: RateProps) {
    const pricingUnits = usePricingUnits();
    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.RateDto>) => (
        <div style={{ display: "flex", gap: "0.25rem" }}>
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)} ariaLabel="Modifier le tarif">
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)} ariaLabel="Supprimer le tarif">
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return <DataTable value={rates} dataKey="id" scrollable style={{ backgroundColor: "var(--color-white)", width: "100%" }} rowClassName={() => "row-separator"} className="data_table">
        <Column field="serviceId" header="Service" style={{ minWidth: "150px" }} body={(rowData) => services.find(s => s.id === rowData.serviceId)?.name ?? rowData.serviceId} />
        <Column field="languages" header="Paire de langues" style={{ minWidth: "150px" }} body={(rowData) => `${languages.find(l => l.id === rowData.sourceLanguageId)?.name ?? rowData.sourceLanguageId} \u2192 ${languages.find(l => l.id === rowData.targetLanguageId)?.name ?? rowData.targetLanguageId}`} />
        <Column field="unitPrice" header="Tarif" style={{ minWidth: "150px" }} body={(rowData) => `${new Intl.NumberFormat("fr-FR", { minimumFractionDigits: 0, maximumFractionDigits: 6 }).format(Number(rowData.unitPrice))} ${directCustomerCurrencySign} ${pricingUnits.find(unit => unit.code === rowData.unit)?.name ?? rowData.unit}`} />
        <Column body={actionsBodyTemplate} header="Actions" headerStyle={{ minWidth: "100px" }} bodyStyle={{ minWidth: "100px", display: "flex", justifyContent: "flex-end", marginRight: "1rem" }} pt={{ headerContent: { style: { justifyContent: "flex-end", marginRight: "1rem" } } }} />
    </DataTable>
}

export default RateDataTable;
