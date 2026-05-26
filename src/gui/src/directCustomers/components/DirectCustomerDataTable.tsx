import "./DirectCustomerDataTable.css";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import Button from "../../components/ui/Button";
import { PencilIcon, DeleteIcon, EyeIcon, CustomersIcon, PlusSignIcon, PhoneIcon, MailIcon } from "../../assets/icons/icons";
import type { schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";

interface DirectCustomerProps {
    directCustomers: zod.infer<typeof schemas.DirectCustomerDto>[];
    onAdd: () => void;
    onView: (id: string) => void;
    onEdit: (id: string) => void;
    onDelete: (id: string) => void;
}

function DirectCustomerDataTable({ directCustomers, onAdd, onView, onEdit, onDelete }: DirectCustomerProps) {

    const contactBodyTemplate = (rowData: zod.infer<typeof schemas.DirectCustomerDto>) => (
        <div>
            <div className="contact_info"><MailIcon size={16} /> <a href={`mailto:${rowData.email}`}>{rowData.email}</a></div>
            {rowData.phone && <div className="contact_info"><PhoneIcon size={16} /> <a href={`tel:${rowData.phone}`}>{rowData.phone}</a></div>}
        </div>
    );

    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.DirectCustomerDto>) => (
        <div style={{ display: "flex", gap: "0.25rem" }}>
            <Button name="" variant="action" type="button" onClick={() => onView(rowData.id)} ariaLabel="Voir le client">
                <EyeIcon size={16} color="var(--color-blue-deep)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)} ariaLabel="Modifier le client">
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)} ariaLabel="Supprimer le client">
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return <>
        {directCustomers.length === 0 ?
            <div className="no_content">
                <CustomersIcon size={32} color="var(--color-slate-400)" />
                <p>Aucun client direct pour le moment.</p>
                <Button variant="light" name="Ajouter un premier client" width="default" type="button" onClick={() => onAdd()}><PlusSignIcon size={16} /></Button>
            </div> : <DataTable value={directCustomers} paginator rows={10} scrollable style={{ width: "100%" }} rowClassName={() => "row-separator"} className="data_table">
                <Column field="name" header="Nom" style={{ minWidth: "200px" }} />
                <Column field="code" header="Code" style={{ minWidth: "100px" }} />
                <Column body={contactBodyTemplate} header="Contact" style={{ minWidth: "200px" }} />
                <Column body={actionsBodyTemplate} header="Actions" headerStyle={{ minWidth: "100px" }} bodyStyle={{ minWidth: "100px", display: "flex", justifyContent: "flex-end", marginRight: "1rem" }} pt={{ headerContent: { style: { justifyContent: "flex-end", marginRight: "1rem" } } }} />
            </DataTable>}
    </>;
}

export default DirectCustomerDataTable;