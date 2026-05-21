import { DataTable } from "primereact/datatable";
import { DeleteIcon, PencilIcon } from "../../assets/icons/icons";
import Button from "../../components/ui/Button";
import type { schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";
import { Column } from "primereact/column";

interface LegalStatusProps {
    legalStatuses: zod.infer<typeof schemas.LegalStatusDto>[];
    onEdit: (id: string) => void;
    onDelete: (id: string) => void;
}

function LegalStatusDataTable({ legalStatuses, onEdit, onDelete }: LegalStatusProps) {
    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.LegalStatusDto>) => (
        <div style={{ display: "flex", alignItems: "center", justifyContent: "flex-end" }}>
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)}>
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)}>
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return <DataTable value={legalStatuses} dataKey="id" scrollable style={{ backgroundColor: "var(--color-white)", width: '100%' }}>
        <Column field="name" header="Type de statut" />
        <Column field="siret" header="SIRET" />
        <Column field="vatNumber" header="Numéro de TVA" />
        <Column field="validFrom" header="Début de validité" body={(rowData) => new Date(rowData.validFrom).toLocaleDateString()} />
        <Column field="status" header="Statut" body={(rowData) => {
            const isInactive = rowData.validTo != null && new Date(rowData.validTo) < new Date();
            return <span style={{ color: isInactive ? "var(--color-red-dark)" : "var(--color-green-dark)", backgroundColor: isInactive ? "var(--color-red-light)" : "var(--color-green-light)", fontWeight: 600, padding: "0.25rem 0.65rem", borderRadius: "999px" }}>{isInactive ? "Inactif" : "★ Actif"}</span>;
        }} />
        <Column body={actionsBodyTemplate} style={{ minWidth: '100px', display: 'flex', justifyContent: 'flex-end', marginRight: '1rem' }} header="Actions" />
    </DataTable>
}

export default LegalStatusDataTable;