import React, { FormEvent, useState, useContext, useEffect } from "react";
import { AccountApi } from "../../services/AccountApi";
import { AppContext } from "../../context/AppContext";
import jwt_decode from "jwt-decode";
import { useHistory } from "react-router-dom";
import { ILoginDTO } from "../../domain/DTO/ILoginDTO";

const Login = () => {
    const history = useHistory();
    const [props, setState] = useState({ email: '', password: '' } as ILoginDTO);
    const appContext = useContext(AppContext);
    const handleChange = (target: EventTarget & HTMLInputElement) => {
        setState({ ...props, [target.name]: target.value });
    }


    //Login through api and handle jwt tokens
    const handleSubmit = (e: FormEvent<HTMLFormElement>) => {
        let jwt: string | null = null;
        AccountApi.getJwt(props).then(data => {
            jwt = data
            if (jwt != null) {
                let jwtDecoded: any = jwt_decode(jwt);
                /* appContext.setUserRole(jwtDecoded['http://schemas.microsoft.com/ws/2008/06/identity/claims/role']);
                appContext.setUserName(jwtDecoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']); */
                appContext.setJwt(jwt);
                if (jwt_decode<any>(jwt)['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] == 'Student') {
                    history.push('/homestudent');
                    return;
                }
                history.push('/');
            }
        });

        e.preventDefault();
    }

    return (
        <>
            <div className="container">
                <h1>Log in</h1>
                <div className="row">
                    <div className="col-md-4">
                        <h4>Use a local account to log in.</h4>

                        <form className="form-group" onSubmit={handleSubmit}>
                            <label htmlFor="email">Email</label>
                            <input className="form-control" type="email" name="email" onChange={(e) => handleChange(e.target)} />
                            <label htmlFor="password">Password</label>
                            <input
                                className="form-control"
                                type="password"
                                name="password"
                                onChange={(e) => handleChange(e.target)}
                            />
                            <button className="btn btn-primary" type="submit">Log in</button>
                        </form>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Login;