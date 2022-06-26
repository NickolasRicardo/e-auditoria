import { Route, Routes } from 'react-router-dom';
import { IPath, paths } from './path.routes';

function CreateRoute(route: IPath, key: number) {
  return <Route key={key} path={route.path} element={route.component} />;
}

function Router() {
  return <Routes>{paths.map((route, key) => CreateRoute(route, key))}</Routes>;
}

export { Router };
