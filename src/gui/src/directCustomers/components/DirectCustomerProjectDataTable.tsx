import * as zod from "zod";
import type { schemas } from "../../infrastructure/openApi/client";
import Button from "../../components/ui/Button";
import { DeleteIcon, PencilIcon } from "../../assets/icons/icons";
import { Column } from "primereact/column";
import { DataTable } from "primereact/datatable";

interface DirectCustomerProjectDataTableProps {
    projects: zod.infer<typeof schemas.ProjectDto>[];
    onEdit: (id: string) => void;
    onDelete: (id: string) => void;
}

function DirectCustomerProjectDataTable({ projects, onEdit, onDelete }: DirectCustomerProjectDataTableProps) {
    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.ProjectDto>) => (
        <div style={{ display: "flex", gap: "0.25rem" }}>
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)} ariaLabel="Modifier le projet">
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)} ariaLabel="Supprimer le projet">
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return (
        <DataTable value={projects} dataKey="id" scrollable style={{ backgroundColor: "var(--color-white)", width: "100%" }} rowClassName={() => "row-separator"} className="data_table">
            <Column field="name" header="Nom du projet" style={{ minWidth: "200px" }} />
            <Column field="domain" header="Domaine" style={{ minWidth: "150px" }} />
            <Column field="endCustomer" header="Client final" style={{ minWidth: "150px" }} />
            <Column field="description" header="Description" style={{ minWidth: "150px" }} />
            <Column body={actionsBodyTemplate} header="Actions" headerStyle={{ minWidth: "100px" }} bodyStyle={{ minWidth: "100px", display: "flex", justifyContent: "flex-end", marginRight: "1rem" }} pt={{ headerContent: { style: { justifyContent: "flex-end", marginRight: "1rem" } } }} />
        </DataTable>
    );
}

export default DirectCustomerProjectDataTable;