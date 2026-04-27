import Button from "../components/ui/Button";

function Homepage() {
    return (
        <div className="homepage">
            <h1>Welcome to the Homepage!</h1>
            <p>This is the main landing page of our application.</p>
            <Button name="Click Me" variant="plain" width="large" onClick={() => alert("Button Clicked!")} />
        </div>
    );
}

export default Homepage;