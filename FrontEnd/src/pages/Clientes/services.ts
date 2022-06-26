import api from '../../http/api';
import moment from 'moment';

export interface ICliente {
  id: number;
  nome: string;
  cpf: string;
  dataNascimento: string;
}

export interface IInterfaceResponse {
  error: boolean;
  response?: ICliente[];
}

export interface IRequestInterface {}

class Services {
  public async ListClientes(): Promise<IInterfaceResponse> {
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

  public DeleteCliente = async (id: number) => {
    return await api
      .delete(`api/Clientes/${id}`)
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

  public UpdateCliente = async (data: ICliente) => {
    return await api
      .put(`api/Clientes/${data.id}`, data)
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

  public CreateCliente = async (data: ICliente) => {
    return await api
      .post(`api/Clientes/`, data)
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
}

export default Services;
