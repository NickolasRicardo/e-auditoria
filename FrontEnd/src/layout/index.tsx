import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { paths } from '../routes/path.routes';
import { Menu } from 'antd';

import * as S from './styles';

interface Props {
  children: React.ReactNode;
}

const Layout: React.FC<Props> = ({ children }) => {
  const [currentPath, setCurrentPath] = useState('');
  let navigate = useNavigate();
  const location = useLocation();

  useEffect(() => {
    setCurrentPath(location.pathname);
  }, [location]);

  return (
    <>
      <Menu mode="horizontal" selectedKeys={[currentPath]}>
        {paths.map(path => (
          <Menu.Item onClick={() => navigate(path.path)} key={path.path}>
            {path.name}
          </Menu.Item>
        ))}
      </Menu>
      <S.Container>{children}</S.Container>
    </>
  );
};

export { Layout };
