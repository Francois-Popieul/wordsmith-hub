import Sidebar from "../components/partials/Sidebar";

function OrderView() {
    return (
        <>
            <div className="orders">
                <h1>Orders Page</h1>
                <p>This page displays all the orders placed by customers.</p>
            </div>
            <Sidebar />
        </>
    );
}

export default OrderView;