import React, { useContext } from "react";
import { Link, useHistory } from "react-router-dom";
import { AppContext } from "../../context/AppContext";
import jwt_decode from "jwt-decode";


const Header = () => {
    const appContext = useContext(AppContext);
    const history = useHistory();

    function logOut(e: React.MouseEvent<HTMLButtonElement, MouseEvent>) {
        appContext.setUserName('')
        appContext.setUserRole('')
        appContext.setJwt(null);
        history.push('/');
        e.preventDefault();
    }

    function isLoggedIn(loggedIn: Boolean) {
        if (loggedIn) {
            return (
                <ul className="navbar-nav">
                    <li className="nav-item" style={{ paddingRight: "10px" }}>
                        {jwt_decode<any>(appContext.jwt!)['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']}
                    </li>
                    <li className="nav-item">
                        <button className="btn btn-primary" onClick={(e) => logOut(e)}>Logout</button>
                    </li>
                </ul>
            );
        }

        return (
            <ul className="navbar-nav">
                <li className="nav-item">
                    <Link to="/login">Login </Link>
                </li>
                <li className="nav-item" style={{ paddingLeft: "10px" }}>
                    <Link to="/register">Register </Link>
                </li>
            </ul>
        );
    }
    function home() {
        return (
            <li className="nav-item" style={{ paddingRight: "10px" }}>
                <Link to="/">Shipments</Link>
            </li>
        );
    }

    return (
        <header>
            <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
                <div className="container">
                    <a className="navbar-brand" href="/">WebApp</a>
                    <button className="navbar-toggler" type="button" data-toggle="collapse" data-target=".navbar-collapse" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span className="navbar-toggler-icon"></span>
                    </button>
                    <div className="navbar-collapse collapse d-sm-inline-flex flex-sm-row-reverse">
                        <ul className="navbar-nav">
                            {isLoggedIn(appContext.jwt != null && appContext.jwt.length > 0)}
                        </ul>

                        <ul className="navbar-nav flex-grow-1">
                            {home()}
                            <li className="nav-item" style={{ paddingRight: "10px" }}>
                                <Link to="/Parcels">Available parcels</Link>
                            </li>
                        </ul>
                    </div>
                </div>
            </nav>
        </header>
        
    );
}

export default Header;