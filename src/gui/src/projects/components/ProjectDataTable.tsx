import "./ProjectDataTable.css";
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import Button from '../../components/ui/Button';
import { PencilIcon, DeleteIcon, ProjectsIcon, PlusSignIcon } from '../../assets/icons/icons';
import type { schemas } from "../../infrastructure/openApi/client";
import * as zod from "zod";

interface ProjectProps {
    projects: zod.infer<typeof schemas.ProjectDto>[];
    onAdd: () => void;
    onEdit: (id: string) => void;
    onDelete: (id: string) => void;
}

function ProjectDataTable({ projects, onAdd, onEdit, onDelete }: ProjectProps) {

    const actionsBodyTemplate = (rowData: zod.infer<typeof schemas.ProjectDto>) => (
        <div style={{ display: "flex", gap: "0.25rem" }}>
            <Button name="" variant="action" type="button" onClick={() => onEdit(rowData.id)}>
                <PencilIcon size={16} color="var(--color-slate-500)" />
            </Button>
            <Button name="" variant="action" type="button" onClick={() => onDelete(rowData.id)}>
                <DeleteIcon size={16} color="var(--color-red-deep)" />
            </Button>
        </div>
    );

    return <>
        {projects.length === 0 ?
            <div className="no_content">
                <ProjectsIcon size={32} color="var(--color-slate-400)" />
                <p>Aucun projet pour le moment.</p>
                <Button variant="light" name="Ajouter un premier projet" width="default" type="button" onClick={() => onAdd()}><PlusSignIcon size={16} /></Button>
            </div> : <DataTable value={projects} paginator rows={10} scrollable style={{ width: '100%' }} rowClassName={() => 'row-separator'} className="project_table">
                <Column field="name" header="Nom" style={{ minWidth: '200px' }} />
                <Column field="directCustomer" header="Client direct" style={{ minWidth: '100px' }} body={(rowData: zod.infer<typeof schemas.ProjectDto>) => rowData.directCustomers ? rowData.directCustomers.map(dc => dc.name).join(", ") : ""} />
                <Column field="endCustomer" header="Client final" style={{ minWidth: '100px' }} body={(rowData: zod.infer<typeof schemas.ProjectDto>) => rowData.endCustomer ? rowData.endCustomer.name : ""} />
                <Column field="status" header="Statut" style={{ minWidth: '100px' }} />
                <Column body={actionsBodyTemplate} header="Actions" headerStyle={{ minWidth: '100px' }} bodyStyle={{ minWidth: '100px', display: 'flex', justifyContent: 'flex-end', marginRight: '1rem' }} pt={{ headerContent: { style: { justifyContent: 'flex-end', marginRight: '1rem' } } }} />
            </DataTable>}
    </>;
}

export default ProjectDataTable;