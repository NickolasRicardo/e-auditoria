import React from 'react';
import { ConfigProvider } from 'antd';
import ptBRAnt from 'antd/lib/locale/pt_BR';

import { Router } from './routes';
import { Layout } from './layout';

import './styles/styles.css';
import { BrowserRouter } from 'react-router-dom';

const App: React.FC = () => {
  return (
    <ConfigProvider locale={ptBRAnt}>
      <BrowserRouter>
        <Layout>
          <Router />
        </Layout>
      </BrowserRouter>
    </ConfigProvider>
  );
};

export { App };
