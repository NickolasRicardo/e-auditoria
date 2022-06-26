import React from 'react';
import { Home } from '../pages/Home';
import { Filmes } from '../pages/Filmes';
import { Clientes } from '../pages/Clientes';
import { Locacoes } from '../pages/Locacoes';

export interface IPath {
  name: string;
  path: string;
  component: React.ReactNode | null;
}

export const paths = [
  {
    name: 'Home',
    path: '/',
    component: <Home />,
  },
  {
    name: 'Clientes',
    path: '/clientes',
    component: <Clientes />,
  },
  {
    name: 'Filmes',
    path: '/filmes',
    component: <Filmes />,
  },
  {
    name: 'Locações',
    path: '/locacoes',
    component: <Locacoes />,
  },
];
