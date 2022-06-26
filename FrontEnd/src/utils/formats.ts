import { format } from 'date-fns';

export const CurrencyFormat = (v: any) => {
  const value = parseFloat(v);
  return new Intl.NumberFormat('pt-br', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

export const FormatDate = (date: string) => {
  return format(new Date(date), 'dd/MM/yyyy');
};
