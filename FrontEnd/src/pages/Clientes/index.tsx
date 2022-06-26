import React, { useEffect, useState } from 'react';

import * as S from './styles';
import Services, { ICliente } from './services';
import {
  Button,
  Col,
  Form,
  Input,
  Modal,
  notification,
  Row,
  Space,
  Table,
} from 'antd';
import type { ColumnsType } from 'antd/lib/table';
import { ExclamationCircleOutlined } from '@ant-design/icons';

type NotificationType = 'success' | 'info' | 'warning' | 'error';

const Clientes: React.FC = () => {
  const [clientes, setClientes] = useState<ICliente[]>([]);

  const [clienteID, setClienteID] = useState(0);

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
      nome: '',
      cpf: '',
      dataNascimento: '',
    });
    setVisible2(true);
  };

  const hideModal2 = () => {
    setVisible2(false);
  };

  const initForm = (data: ICliente) => {
    let dataSplit = data.dataNascimento.split('/');

    let dataAniv = new Date(
      parseInt(dataSplit[2]),
      parseInt(dataSplit[1]) - 1,
      parseInt(dataSplit[0])
    )
      .toISOString()
      .substring(0, 10);

    form.setFieldsValue({
      nome: data.nome,
      cpf: data.cpf,
      dataNascimento: dataAniv,
    });
  };

  const columns: ColumnsType<ICliente> = [
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
    {
      title: 'CPF',
      dataIndex: 'cpf',
      key: 'cpf',
    },
    {
      title: 'Data de Nascimento',
      dataIndex: 'dataNascimento',
      key: 'dataNascimento',
    },
    {
      title: 'Ações',
      key: 'action',
      render: (_, cliente) => (
        <Space size="middle">
          <a
            onClick={() => {
              initForm(cliente);
              setClienteID(cliente.id);
              showModal();
            }}
          >
            Editar
          </a>
          <a onClick={() => confirm(cliente)}>Deletar</a>
        </Space>
      ),
    },
  ];

  const onFinish = (newCliente: boolean) => {
    const values = form.getFieldsValue();

    //console.log('Received values of form: ', values);

    if (newCliente) {
      CreateCliente(values);
    } else {
      UpdateCliente(values);
    }
  };

  const LoadClientes = async () => {
    let services = new Services();
    setLoading(true);

    const { error, response } = await services.ListClientes();

    if (!error && response) {
      setClientes(response);
      setLoading(false);
    }
  };

  const DeleteCliente = async (id: number) => {
    let services = new Services();

    const { error } = await services.DeleteCliente(id);

    if (!error) {
      LoadClientes();
      openNotificationWithIcon('success', 'Cliente deletado com sucesso!');
    } else {
      openNotificationWithIcon('error', 'Erro ao deletar cliente!');
    }
  };

  const CreateCliente = async (data: ICliente) => {
    let services = new Services();

    const { error } = await services.CreateCliente(data);

    if (!error) {
      openNotificationWithIcon('success', 'Cliente adicionado com sucesso!');
      LoadClientes();

      hideModal2();
    } else {
      openNotificationWithIcon('error', 'Erro ao adicionar cliente!');
    }
  };

  const UpdateCliente = async (data: ICliente) => {
    let services = new Services();

    data.id = clienteID;

    const { error } = await services.UpdateCliente(data);

    if (!error) {
      openNotificationWithIcon('success', 'Cliente atualizado com sucesso!');
      LoadClientes();

      hideModal();
    } else {
      openNotificationWithIcon('error', 'Erro ao atualizar cliente!');
    }
  };

  const confirm = (data: ICliente) => {
    Modal.confirm({
      title: 'Excluir',
      icon: <ExclamationCircleOutlined />,
      content: 'Você realmente deseja excluir o cliente: ' + data.nome,
      okText: 'Excluir',
      cancelText: 'Cancelar',
      onOk: () => {
        DeleteCliente(data.id);
      },
    });
  };

  useEffect(() => {
    LoadClientes();
  }, []);

  return (
    <S.Container>
      <Row>
        <Col span={12}>
          <h1>Clientes</h1>
        </Col>
        <Col span={6}></Col>
        <Col span={6}>
          <Button style={{ width: '100%', height: '80%' }} onClick={showModal2}>
            Novo Cliente
          </Button>
        </Col>
        <Col span={24}>
          <Table
            columns={columns}
            dataSource={clientes}
            loading={loading}
            pagination={{ defaultPageSize: 10 }}
          />
        </Col>
      </Row>

      <Modal
        title="Atualizar Cliente"
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
          name="clienteEdit"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item name="nome" label="Nome">
            <Input />
          </Form.Item>
          <Form.Item name="cpf" label="CPF">
            <Input />
          </Form.Item>
          <Form.Item name="dataNascimento" label="Data de Nascimento">
            <Input type="date" />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="Novo Cliente"
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
          name="clienteCreate"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item name="nome" label="Nome">
            <Input />
          </Form.Item>
          <Form.Item name="cpf" label="CPF">
            <Input />
          </Form.Item>
          <Form.Item name="dataNascimento" label="Data de Nascimento">
            <Input type="date" />
          </Form.Item>
        </Form>
      </Modal>
    </S.Container>
  );
};

export { Clientes };
