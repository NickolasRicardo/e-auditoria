import api from '../../http/api';

interface IRelatorio {
  quantidade: number | undefined;
  data: [];
}

export interface IInterfaceResponse {
  error: boolean;
  response?: IRelatorio;
}

export interface IInterfaceResponseWithoutQuantidade {
  error: boolean;
  response?: [];
}

export interface IRequestInterface {}

class Services {
  public async GetClientesEmAtraso(): Promise<IInterfaceResponse> {
    return await api
      .get<IRelatorio>('/api/Relatorio/ClientesEmAtraso')
      .then(response => {
        let resposta: IRelatorio = {} as IRelatorio;

        resposta.data = response.data.data;
        resposta.quantidade = response.data.quantidade;

        return {
          error: false,
          response: resposta,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  }
  public async GetFilmesNaoAlugados(): Promise<IInterfaceResponse> {
    return await api
      .get<IRelatorio>('/api/Relatorio/FilmesNaoAlugados')
      .then(response => {
        let resposta: IRelatorio = {} as IRelatorio;

        resposta.data = response.data.data;
        resposta.quantidade = response.data.quantidade;

        return {
          error: false,
          response: resposta,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  }
  public async GetFilmesMaisAlugados(): Promise<IInterfaceResponseWithoutQuantidade> {
    return await api
      .get('/api/Relatorio/FilmesMaisAlugados')
      .then(response => {
        return {
          error: false,
          response: response.data,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  }
  public async GetFilmesMenosAlugados(): Promise<IInterfaceResponseWithoutQuantidade> {
    return await api
      .get('/api/Relatorio/FilmesMenosAlugados')
      .then(response => {
        return {
          error: false,
          response: response.data,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  }
  public async GetClienteQueMaisAlugou(): Promise<IInterfaceResponseWithoutQuantidade> {
    return await api
      .get('/api/Relatorio/ClienteQueMaisAlugou')
      .then(response => {
        let array = [] as any;
        array.push(response.data);
        return {
          error: false,
          response: array,
        };
      })
      .catch(error => {
        return {
          error: true,
        };
      });
  }
}

export default Services;
