import Sidebar from "../partials/Sidebar";
import "./AppLayout.css";

interface AppLayoutProps {
    children: React.ReactNode;
}

function AppLayout(props: AppLayoutProps) {
    return (
        <main className="app_layout">
            <section>
                <Sidebar />
            </section>
            <section className="app_right_panel">
                {props.children}
            </section>
        </main >
    );
}

export default AppLayout;