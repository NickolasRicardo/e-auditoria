import React from 'react';
import { Space, Table } from 'antd';
// import { Container } from './styles';

interface TableProps {
  quantidade: number | undefined;
  data: [];
}

const TabelaRelatorio: React.FC<TableProps> = ({ quantidade, data }) => {
  const columns = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Nome',
      dataIndex: 'nome',
      key: 'nome',
    },
  ];

  return (
    <>
      {quantidade && <>Quantidade: {quantidade}</>}
      <Table columns={columns} dataSource={data} />
    </>
  );
};

export default TabelaRelatorio;
