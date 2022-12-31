import { createContext } from "react";

export let UserRoleContext = createContext({});

export const FormContext = createContext({
  formSubmitted: false,
  setFormSubmitted: () => {},
});
