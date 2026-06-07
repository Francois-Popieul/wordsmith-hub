import * as zod from "zod";
import type { schemas } from "../../infrastructure/openApi/client";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import Button from "../../components/ui/Button";
import { DeleteIcon, OrdersIcon, PencilIcon, PlusSignIcon } from "../../assets/icons/icons";
import clsx from "clsx";
import "./OrderDataTable.css";

interface OrderProps {
    workOrders: zod.infer<typeof schemas.WorkOrderDto>[];
    workOrderStatuses: zod.infer<typeof schemas.Status>[];
    onAdd: () => void;
    onEdit: (id: string) => void;
    onStatusChange: (id: string, statusId: string) => void;
    onDelete: (id: string) => void;
}

function OrderDataTable({ workOrders: orders, workOrderStatuses, onAdd, onEdit, onStatusChange, onDelete }: OrderProps) {

    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.WorkOrderDto>) => (
        <div style={{ display: "flex", gap: "0.25rem" }}>
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)} ariaLabel="Modifier la commande">
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)} ariaLabel="Supprimer la commande">
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return <>
        {orders.length === 0 ?
            <div className="no_content">
                <OrdersIcon size={32} color="var(--color-slate-400)" />
                <p>Aucune commande pour le moment.</p>
                <Button variant="light" name="Ajouter une première commande" width="default" type="button" onClick={() => onAdd()}><PlusSignIcon size={16} /></Button>
            </div> : <DataTable value={orders} paginator rows={10} scrollable style={{ width: "100%" }} rowClassName={() => "row-separator"} className="data_table">
                <Column field="reference" header="Référence" style={{ minWidth: "200px" }} />
                <Column field="project" header="Projet" style={{ minWidth: "150px" }} body={(rowData: zod.infer<typeof schemas.WorkOrderDto>) => rowData.project ? rowData.project.name : ""} />
                <Column field="directCustomer" header="Client" style={{ minWidth: "150px" }} body={(rowData: zod.infer<typeof schemas.WorkOrderDto>) => rowData.directCustomer ? rowData.directCustomer.name : ""} />
                <Column field="status" header="Statut" style={{ minWidth: "100px" }} body={(rowData: zod.infer<typeof schemas.WorkOrderDto>) => (
                    <select id="status-select" name="workOrder-status" className={clsx(`status_select_${rowData.statusId}`)} onChange={(e) => onStatusChange(rowData.id, e.target.value)} value={rowData.statusId}>
                        {workOrderStatuses.map(status => (<option key={status.id} value={status.id} selected={status.id === rowData.statusId}>{status.name}</option>
                        ))}
                    </select>
                )} />
                <Column body={actionsBodyTemplate} header="Actions" headerStyle={{ minWidth: "100px" }} bodyStyle={{ minWidth: "100px", display: "flex", justifyContent: "flex-end", marginRight: "1rem" }} pt={{ headerContent: { style: { justifyContent: "flex-end", marginRight: "1rem" } } }} />
            </DataTable>}
    </>;
}

export default OrderDataTable;