import api from '../../http/api';

export interface IInterfaceResponse {}

export interface IRequestInterface {}

class Services {
  public async UpdatePessoaPerfilByPessoaId(data: IRequestInterface) {
    return await api
      .post<IInterfaceResponse>('', data)
      .then(response => {
        return {
          error: false,
          response: response.data,
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
