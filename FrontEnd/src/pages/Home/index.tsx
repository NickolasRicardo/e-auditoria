import React, { useState } from 'react';
import * as S from './styles';
import TabelaRelatorio from './../../components/TabelaRelatorio';
import { Col, Form, Row, Select } from 'antd';
import Services from './services';
const { Option } = Select;

const Home: React.FC = () => {
  const opcoes = [
    { id: 1, nome: 'Clientes em atraso' },
    { id: 2, nome: 'Filmes nunca alugados' },
    { id: 3, nome: 'Filmes mais alugados no ano passado' },
    { id: 4, nome: 'Filmes menos alugados na ultima semana' },
    { id: 5, nome: 'Segundo Cliente que mais alugou filmes' },
  ];
  const [opcaoSelecionada, setOpcaoSelecionada] = useState(0);
  const [data, setData] = useState<[]>([]);
  const [quantidade, setQuantidade] = useState<number | undefined>(undefined);

  const LoadingRelatorio = async (e: any) => {
    let services = new Services();
    if (e === 1) {
      let { error, response } = await services.GetClientesEmAtraso();
      if (!error && response) {
        setData(response.data);
        setQuantidade(response.quantidade);
      }
    }
    if (e === 2) {
      let { error, response } = await services.GetFilmesNaoAlugados();
      if (!error && response) {
        setData(response.data);
        setQuantidade(response.quantidade);
      }
    }
    if (e === 3) {
      let { error, response } = await services.GetFilmesMaisAlugados();
      if (!error && response) {
        setData(response);
      }
    }
    if (e === 4) {
      let { error, response } = await services.GetFilmesMenosAlugados();
      if (!error && response) {
        setData(response);
      }
    }
    if (e === 5) {
      let { error, response } = await services.GetClienteQueMaisAlugou();
      if (!error && response) {
        setData(response);
      }
    }
  };

  return (
    <S.Container>
      <Row>
        <Col span={6}>
          <h1 style={{ width: '100%' }}>Relatórios</h1>
        </Col>
        <Col span={6}>
          <Select
            optionFilterProp="children"
            style={{ minWidth: '100%', padding: 5 }}
            showSearch
            placeholder={'Selecione o relatório desejado'}
            onSelect={(e: any) => LoadingRelatorio(e)}
          >
            {opcoes &&
              opcoes.map((item, index) => {
                return (
                  <Option key={index} value={item.id}>
                    {item.nome}
                  </Option>
                );
              })}
          </Select>
        </Col>
        <Col span={6}></Col>
        <Col span={12}>
          <TabelaRelatorio data={data} quantidade={quantidade} />
        </Col>
      </Row>
    </S.Container>
  );
};

export { Home };
