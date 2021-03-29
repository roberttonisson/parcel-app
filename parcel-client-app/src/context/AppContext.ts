import React from "react";

export interface IAppContext {
    jwt: string | null;
    userName: string;
    userRole: string;
    setJwt: (jwt: string | null) => void;
    setUserName: (userName: string) => void;
    setUserRole: (userRole: string) => void;
}

export const AppContextInitialState: IAppContext = {
    jwt: null,
    userName: '',
    userRole: '',
    setJwt: (x) => {},
    setUserName: (x) => {},
    setUserRole: (x) => {}
}

export const AppContext = React.createContext<IAppContext>(AppContextInitialState);

export const AppContextProvider = AppContext.Provider;
export const AppContextConsumer = AppContext.Consumer;