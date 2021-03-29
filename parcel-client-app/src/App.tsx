import { useState } from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import { AppContextProvider, AppContextInitialState, IAppContext } from "./context/AppContext";
import Home from "./components/Home";
import Header from "./components/shared/Header";
import Login from "./components/account/Login";
import Register from "./components/account/Register";
import ShipmentEdit from "./components/views/ShipmentEdit";
import Parcels from "./components/views/Parcels";

const App = () => {
    const setJwt = (jwt: string | null) => {
        setAppState({ ...appState, jwt: jwt });
    }

    const setUserName = (userName: string) => {
        setAppState({ ...appState, userName: userName });
    }

    const setUserRole = (userRole: string) => {
        setAppState({ ...appState, userRole: userRole });
    }

    const initialAppState = {
        ...AppContextInitialState,
        setJwt,
        setUserName,
        setUserRole,
    } as IAppContext;
    const [appState, setAppState] = useState(initialAppState);

    return (
        <AppContextProvider value={appState}>
            <Router>
                <Header />
                <Switch>
                    <Route exact path="/">
                        <Home />
                    </Route>
                    <Route path="/home">
                        <Home />
                    </Route>
                    <Route path="/shipment/:shipmentNumber">
                        <ShipmentEdit />
                    </Route>
                    <Route path="/parcels">
                        <Parcels />
                    </Route>
                    <Route path="/login">
                        <Login />
                    </Route>
                    <Route path="/register">
                        <Register />
                    </Route>
                    <h1>Page not found 404</h1>
                </Switch>
            </Router>
        </AppContextProvider>
    );
};

export default App;
