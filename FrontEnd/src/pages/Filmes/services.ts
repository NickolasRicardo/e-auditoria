import api from '../../http/api';

export interface IFilme {
  id: number;
  titulo: string;
  classificacao: any;
  lancamento: boolean;
}

export interface IInterfaceResponse {
  error: boolean;
  response?: IFilme[];
}

export interface IRequestInterface {}

class Services {
  public async ListFilmes(): Promise<IInterfaceResponse> {
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

  public DeleteFilme = async (id: number) => {
    return await api
      .delete(`api/Filmes/${id}`)
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

  public UpdateFilme = async (data: IFilme) => {
    return await api
      .put(`api/Filmes/${data.id}`, data)
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

  public CreateFilme = async (data: IFilme) => {
    return await api
      .post(`api/Filmes/`, data)
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
