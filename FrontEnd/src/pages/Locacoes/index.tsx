import React, { useEffect, useState } from 'react';

import * as S from './styles';
import Services, { ILocacao, IRequestInterface } from './services';
import {
  Button,
  Col,
  Form,
  Input,
  Modal,
  notification,
  Row,
  Select,
  Space,
  Table,
} from 'antd';

import type { ColumnsType } from 'antd/lib/table';
import { ExclamationCircleOutlined } from '@ant-design/icons';
import { IFilme } from '../Filmes/services';
import { ICliente } from '../Clientes/services';
const { Option } = Select;

type NotificationType = 'success' | 'info' | 'warning' | 'error';

const Locacoes: React.FC = () => {
  const [Locacoes, setLocacoes] = useState<ILocacao[]>([]);

  const [clientes, setClientes] = useState<ICliente[] | []>([]);
  const [filmes, setFilmes] = useState<IFilme[]>([]);

  const [LocacaoID, setLocacaoID] = useState(0);

  const [visible, setVisible] = useState(false);
  const [visible2, setVisible2] = useState(false);
  const [form] = Form.useForm();
  const [loading, setLoading] = useState(false);

  const openNotificationWithIcon = (
    type: NotificationType,
    message?: string,
    description?: string
  ) => {
    notification[type]({
      message: message,
      description: description,
    });
  };

  const showModal = () => {
    setVisible(true);
  };

  const hideModal = () => {
    setVisible(false);
  };

  const showModal2 = () => {
    form.setFieldsValue({
      idFilme: undefined,
      idCliente: undefined,
      dataLocacao: undefined,
    });
    setVisible2(true);
  };

  const hideModal2 = () => {
    setVisible2(false);
  };

  const initForm = (data: ILocacao) => {
    console.log(data);
    let dataSplit = data.dataLocacao.split('/');

    let dataLocacao = new Date(
      parseInt(dataSplit[2]),
      parseInt(dataSplit[1]) - 1,
      parseInt(dataSplit[0])
    )
      .toISOString()
      .substring(0, 10);
    let dataDevolucao = '';

    if (data.dataDevolucao.trim() !== '-') {
      let dataSplit = data.dataDevolucao.split('/');

      dataDevolucao = new Date(
        parseInt(dataSplit[2]),
        parseInt(dataSplit[1]) - 1,
        parseInt(dataSplit[0])
      )
        .toISOString()
        .substring(0, 10);
    }

    form.setFieldsValue({
      idFilme: data.filmeID,
      idCliente: data.clienteID,
      dataLocacao: dataLocacao,
      dataDevolucao: dataDevolucao,
    });
  };

  const loadFilmes = async () => {
    let services = new Services();

    let { error, response } = await services.ListFilmes();

    if (!error && response) {
      setFilmes(response);
    }
  };

  const loadClientes = async () => {
    let services = new Services();

    let { error, response } = await services.ListClientes();

    if (!error && response) {
      setClientes(response);
    }
  };

  const columns: ColumnsType<ILocacao> = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Cliente',
      dataIndex: 'clienteNome',
      key: 'clienteNome',
    },
    {
      title: 'Filme',
      dataIndex: 'filmeNome',
      key: 'filmeNome',
    },
    {
      title: 'Data de Locação',
      dataIndex: 'dataLocacao',
      key: 'dataLocacao',
    },
    {
      title: 'Data de Devolução',
      dataIndex: 'dataDevolucao',
      key: 'dataDevolucao',
    },
    {
      title: 'Status',
      dataIndex: 'statusDevolucao',
      key: 'statusDevolucao',
    },

    {
      title: 'Ações',
      key: 'action',
      render: (_, Locacao) => (
        <Space size="middle">
          <a
            onClick={() => {
              initForm(Locacao);
              setLocacaoID(Locacao.id);
              showModal();
            }}
          >
            Editar
          </a>
          <a onClick={() => confirm(Locacao)}>Deletar</a>
        </Space>
      ),
    },
  ];

  const onFinish = (newLocacao: boolean) => {
    const values = form.getFieldsValue();

    console.log('Received values of form: ', values);

    if (newLocacao) {
      CreateLocacao(values);
    } else {
      UpdateLocacao(values);
    }
  };

  const LoadLocacoes = async () => {
    let services = new Services();
    setLoading(true);

    const { error, response } = await services.ListLocacoes();

    if (!error && response) {
      setLocacoes(response);
      setLoading(false);
    }
  };

  const DeleteLocacao = async (id: number) => {
    let services = new Services();

    const { error } = await services.DeleteLocacao(id);

    if (!error) {
      LoadLocacoes();
      openNotificationWithIcon('success', 'Locacao deletado com sucesso!');
    } else {
      openNotificationWithIcon('error', 'Erro ao deletar Locacao!');
    }
  };

  const CreateLocacao = async (data: IRequestInterface) => {
    let services = new Services();

    console.log(data);

    const { error } = await services.CreateLocacao(data);
    if (!error) {
      openNotificationWithIcon('success', 'Locacao adicionado com sucesso!');
      LoadLocacoes();
      hideModal2();
    } else {
      openNotificationWithIcon('error', 'Erro ao adicionar Locacao!');
    }
  };

  const UpdateLocacao = async (data: IRequestInterface) => {
    let services = new Services();
    data.id = LocacaoID;

    data.dataDevolucao =
      data.dataDevolucao?.toString() === '' ? undefined : data.dataDevolucao;

    console.log(data.dataDevolucao);

    const { error } = await services.UpdateLocacao(data);
    if (!error) {
      openNotificationWithIcon('success', 'Locacao atualizado com sucesso!');
      LoadLocacoes();
      hideModal();
    } else {
      openNotificationWithIcon('error', 'Erro ao atualizar Locacao!');
    }
  };

  const confirm = (data: ILocacao) => {
    Modal.confirm({
      title: 'Excluir',
      icon: <ExclamationCircleOutlined />,
      content: 'Você realmente deseja excluir a locação: ' + data.id,
      okText: 'Excluir',
      cancelText: 'Cancelar',
      onOk: () => {
        DeleteLocacao(data.id);
      },
    });
  };

  useEffect(() => {
    LoadLocacoes();
    loadClientes();
    loadFilmes();
  }, []);

  return (
    <S.Container>
      <Row>
        <Col span={12}>
          <h1>Locações</h1>
        </Col>
        <Col span={6}></Col>
        <Col span={6}>
          <Button style={{ width: '100%', height: '80%' }} onClick={showModal2}>
            Nova Locação
          </Button>
        </Col>
        <Col span={24}>
          <Table
            columns={columns}
            dataSource={Locacoes}
            loading={loading}
            pagination={{ defaultPageSize: 10 }}
          />
        </Col>
      </Row>

      <Modal
        title="Atualizar Locacao"
        visible={visible}
        onCancel={hideModal}
        cancelText="Cancelar"
        okText="Atualizar"
        closable={false}
        onOk={() => {
          onFinish(false);
        }}
      >
        <Form
          form={form}
          name="LocacaoEdit"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item name="idCliente" label="Clientes">
            <Select optionFilterProp="children" showSearch>
              {clientes &&
                clientes.map((cliente, i) => {
                  return (
                    <Option key={i} value={cliente.id}>
                      {cliente.nome}
                    </Option>
                  );
                })}
            </Select>
          </Form.Item>
          <Form.Item name="idFilme" label="Filmes">
            <Select optionFilterProp="children" showSearch>
              {filmes &&
                filmes.map((filme, i) => {
                  return (
                    <Option key={i} value={filme.id}>
                      {filme.titulo}
                    </Option>
                  );
                })}
            </Select>
          </Form.Item>
          <Form.Item name="dataLocacao" label="Data de Locação">
            <Input type="date" />
          </Form.Item>
          <Form.Item name="dataDevolucao" label="Data de Devolução">
            <Input type="date" />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="Novo Locacao"
        visible={visible2}
        onCancel={hideModal2}
        cancelText="Cancelar"
        okText="Salvar"
        closable={false}
        onOk={() => {
          onFinish(true);
        }}
      >
        <Form
          form={form}
          name="LocacaoCreate"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item name="idCliente" label="Clientes">
            <Select optionFilterProp="children" showSearch>
              {clientes &&
                clientes.map((cliente, i) => {
                  return (
                    <Option key={i} value={cliente.id}>
                      {cliente.nome}
                    </Option>
                  );
                })}
            </Select>
          </Form.Item>
          <Form.Item name="idFilme" label="Filmes">
            <Select optionFilterProp="children" showSearch>
              {filmes &&
                filmes.map((filme, i) => {
                  return (
                    <Option key={i} value={filme.id}>
                      {filme.titulo}
                    </Option>
                  );
                })}
            </Select>
          </Form.Item>
          <Form.Item name="dataLocacao" label="Data de Locação">
            <Input type="date" />
          </Form.Item>
        </Form>
      </Modal>
    </S.Container>
  );
};

export { Locacoes };
