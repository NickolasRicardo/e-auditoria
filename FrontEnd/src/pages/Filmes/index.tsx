import React, { useEffect, useState } from 'react';

import * as S from './styles';
import Services, { IFilme } from './services';
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

const Filmes: React.FC = () => {
  const [Filmes, setFilmes] = useState<IFilme[]>([]);

  const [FilmeID, setFilmeID] = useState(0);

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
      titulo: '',
      classificacao: '',
      lancamento: false,
    });
    setVisible2(true);
  };

  const hideModal2 = () => {
    setVisible2(false);
  };

  const initForm = (data: IFilme) => {
    console.log(data);

    form.setFieldsValue({
      titulo: data.titulo,
      classificacao: data.classificacao === 'Livre' ? 0 : data.classificacao,
      lancamento: data.lancamento,
    });
  };

  const columns: ColumnsType<IFilme> = [
    {
      title: 'ID',
      dataIndex: 'id',
      key: 'id',
    },
    {
      title: 'Titulo',
      dataIndex: 'titulo',
      key: 'titulo',
    },
    {
      title: 'Classificação',
      dataIndex: 'classificacao',
      key: 'classificacao',
    },

    {
      title: 'Ações',
      key: 'action',
      render: (_, Filme) => (
        <Space size="middle">
          <a
            onClick={() => {
              initForm(Filme);
              setFilmeID(Filme.id);
              showModal();
            }}
          >
            Editar
          </a>
          <a onClick={() => confirm(Filme)}>Deletar</a>
        </Space>
      ),
    },
  ];

  const onFinish = (newFilme: boolean) => {
    const values = form.getFieldsValue();

    //console.log('Received values of form: ', values);

    if (newFilme) {
      CreateFilme(values);
    } else {
      UpdateFilme(values);
    }
  };

  const LoadFilmes = async () => {
    let services = new Services();
    setLoading(true);

    const { error, response } = await services.ListFilmes();

    if (!error && response) {
      setFilmes(response);
      setLoading(false);
    }
  };

  const DeleteFilme = async (id: number) => {
    let services = new Services();

    const { error } = await services.DeleteFilme(id);

    if (!error) {
      LoadFilmes();
      openNotificationWithIcon('success', 'Filme deletado com sucesso!');
    } else {
      openNotificationWithIcon('error', 'Erro ao deletar Filme!');
    }
  };

  const CreateFilme = async (data: IFilme) => {
    let services = new Services();
    data.lancamento = data.lancamento.toString() === 'true' ? true : false;

    const { error } = await services.CreateFilme(data);

    if (!error) {
      openNotificationWithIcon('success', 'Filme adicionado com sucesso!');
      LoadFilmes();

      hideModal2();
    } else {
      openNotificationWithIcon('error', 'Erro ao adicionar Filme!');
    }
  };

  const UpdateFilme = async (data: IFilme) => {
    let services = new Services();

    data.id = FilmeID;
    data.lancamento = data.lancamento.toString() === 'true' ? true : false;

    const { error } = await services.UpdateFilme(data);

    if (!error) {
      openNotificationWithIcon('success', 'Filme atualizado com sucesso!');
      LoadFilmes();

      hideModal();
    } else {
      openNotificationWithIcon('error', 'Erro ao atualizar Filme!');
    }
  };

  const confirm = (data: IFilme) => {
    Modal.confirm({
      title: 'Excluir',
      icon: <ExclamationCircleOutlined />,
      content: 'Você realmente deseja excluir o Filme: ' + data.titulo,
      okText: 'Excluir',
      cancelText: 'Cancelar',
      onOk: () => {
        DeleteFilme(data.id);
      },
    });
  };

  useEffect(() => {
    LoadFilmes();
  }, []);

  return (
    <S.Container>
      <Row>
        <Col span={12}>
          <h1>Filmes</h1>
        </Col>
        <Col span={6}></Col>
        <Col span={6}>
          <Button style={{ width: '100%', height: '80%' }} onClick={showModal2}>
            Novo Filme
          </Button>
        </Col>
        <Col span={24}>
          <Table
            columns={columns}
            dataSource={Filmes}
            loading={loading}
            pagination={{ defaultPageSize: 10 }}
          />
        </Col>
      </Row>

      <Modal
        title="Atualizar Filme"
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
          name="FilmeEdit"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item name="titulo" label="Titulo">
            <Input />
          </Form.Item>
          <Form.Item name="classificacao" label="Classificação">
            <Input type="number" />
          </Form.Item>
          <Form.Item name="lancamento" label="Lançamento">
            <Input type="checkbox" />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        title="Novo Filme"
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
          name="FilmeCreate"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item name="titulo" label="Titulo">
            <Input />
          </Form.Item>
          <Form.Item name="classificacao" label="Classificação">
            <Input type="number" />
          </Form.Item>
          <Form.Item name="lancamento" label="Lançamento">
            <Input type="checkbox" />
          </Form.Item>
        </Form>
      </Modal>
    </S.Container>
  );
};

export { Filmes };
