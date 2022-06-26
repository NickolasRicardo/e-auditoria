import api from '../../http/api';

import { IFilme } from '../Filmes/services';
import { ICliente } from '../Clientes/services';
import moment from 'moment';

export interface ILocacao {
  id: number;
  clienteID: number;
  clienteNome: string;
  filmeID: number;
  filmeNome: string;
  dataLocacao: string;
  dataDevolucao: string;
  statusDevolucao: string;
}

export interface IInterfaceResponse {
  error: boolean;
  response?: ILocacao[];
}

export interface IRequestInterface {
  id?: number;
  idCliente: number;
  idFilme: number;
  dataLocacao?: Date;
  dataDevolucao?: Date;
}

export interface IResponseCliente {
  error: boolean;
  response?: ICliente[];
}

export interface IResponseFilme {
  error: boolean;
  response?: IFilme[];
}

class Services {
  public async ListLocacoes(): Promise<IInterfaceResponse> {
    return await api
      .get(`api/Locacaos`)
      .then(response => {
        let clientes = response.data;

        let itemClientes: ILocacao[] = [];

        clientes.map((item: ILocacao) => {
          return itemClientes.push({
            id: item.id,
            clienteID: item.clienteID,
            clienteNome: item.clienteNome,
            filmeID: item.filmeID,
            filmeNome: item.filmeNome,
            dataLocacao: item.dataLocacao,
            dataDevolucao: item.dataDevolucao,
            statusDevolucao: item.statusDevolucao,
          });
        });

        return {
          error: false,
          response: itemClientes,
        };
      })
      .catch(error => {
        return {
          error: true,
          response: [],
        };
      });
  }

  public DeleteLocacao = async (id: number) => {
    return await api
      .delete(`api/Locacaos/${id}`)
      .then(response => {
        return {
          error: false,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  };

  public UpdateLocacao = async (data: IRequestInterface) => {
    return await api
      .put(`api/Locacaos/${data.id}`, data)
      .then(response => {
        return {
          error: false,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  };

  public CreateLocacao = async (data: IRequestInterface) => {
    return await api
      .post(`api/Locacaos/`, data)
      .then(response => {
        return {
          error: false,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  };

  public async ListClientes(): Promise<IResponseCliente> {
    return await api
      .get(`api/Clientes`)
      .then(response => {
        let clientes = response.data;

        let itemClientes: ICliente[] = [];

        clientes.map((item: ICliente) => {
          return itemClientes.push({
            id: item.id,
            cpf: item.cpf,
            dataNascimento: moment(item.dataNascimento).format('DD/MM/yyyy'),
            nome: item.nome,
          });
        });

        return {
          error: false,
          response: itemClientes,
        };
      })
      .catch(error => {
        return {
          error: true,
          response: [],
        };
      });
  }

  public async ListFilmes(): Promise<IResponseFilme> {
    return await api
      .get(`api/Filmes`)
      .then(response => {
        let clientes = response.data;

        let itemClientes: IFilme[] = [];

        clientes.map((item: IFilme) => {
          return itemClientes.push({
            id: item.id,
            lancamento: item.lancamento,
            classificacao:
              item.classificacao === 0 ? 'Livre' : item.classificacao,
            titulo: item.titulo,
          });
        });

        return {
          error: false,
          response: itemClientes,
        };
      })
      .catch(error => {
        return {
          error: true,
          response: [],
        };
      });
  }
}

export default Services;
