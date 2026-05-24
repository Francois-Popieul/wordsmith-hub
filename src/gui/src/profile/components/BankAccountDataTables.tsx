import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import Button from "../../components/ui/Button";
import { PencilIcon, DeleteIcon, StarIcon } from "../../assets/icons/icons";
import type { schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";

interface BankAccountProps {
    bankAccounts: zod.infer<typeof schemas.BankAccountDto>[];
    onDefaultBankChange: (id: string) => void;
    onEdit: (id: string) => void;
    onDelete: (id: string) => void;
}

function BankAccountDataTables({ bankAccounts, onDefaultBankChange: onDefaultChange, onEdit, onDelete }: BankAccountProps) {

    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.BankAccountDto>) => (
        <div style={{ display: "flex", gap: "0.25rem" }}>
            {!rowData.isDefault && <Button name="" variant="action" type="button" onClick={() => onDefaultChange(rowData.id)}>
                <StarIcon size={16} color="var(--color-slate-500)" />
            </Button>}
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)}>
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)}>
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return <DataTable value={bankAccounts} dataKey="id" scrollable style={{ backgroundColor: "var(--color-white)", width: "100%" }} rowClassName={() => "row-separator"} className="data_table">
        <Column field="label" header="Intitulé du compte" />
        <Column field="bankName" header="Banque" />
        <Column field="accountHolderName" header="Titulaire du compte" />
        <Column field="iban" header="IBAN" />
        <Column field="status" header="Statut" body={(rowData) => {
            const isDefault = rowData.isDefault;
            return <span style={{ color: isDefault ? "var(--color-blue-dark)" : "", backgroundColor: isDefault ? "var(--color-blue-light)" : "", fontWeight: 600, padding: "0.25rem 0.65rem", borderRadius: "999px" }}>{isDefault ? "★ Par défaut" : ""}</span>;
        }} />
        <Column body={actionsBodyTemplate} header="Actions" headerStyle={{ minWidth: "100px" }} bodyStyle={{ minWidth: "100px", display: "flex", justifyContent: "flex-end", marginRight: "1rem" }} pt={{ headerContent: { style: { justifyContent: "flex-end", marginRight: "1rem" } } }} />
    </DataTable>
}

export default BankAccountDataTables;