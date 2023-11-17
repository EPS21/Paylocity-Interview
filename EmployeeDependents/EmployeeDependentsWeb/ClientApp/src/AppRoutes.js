import Employee from "./components/Employee";
import { Home } from "./components/Home";

const AppRoutes = [
  {
    index: true,
    element: <Employee />
  },
  {
    path: '/about',
    element: <Home />
  }
];

export default AppRoutes;
