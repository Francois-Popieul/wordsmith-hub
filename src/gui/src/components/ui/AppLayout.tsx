import Sidebar from "../partials/Sidebar";
import TopBar from "../partials/TopBar";
import "./AppLayout.css";

interface AppLayoutProps {
    children: React.ReactNode;
}

function AppLayout(props: AppLayoutProps) {
    return (<>
        <TopBar />
        <main className="app_layout">
            <section>
                <Sidebar />
            </section>
            <section className="app_right_panel">
                {props.children}
            </section>
        </main >
    </>
    );
}

export default AppLayout;