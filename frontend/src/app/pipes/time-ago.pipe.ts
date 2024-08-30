import { Pipe, PipeTransform } from '@angular/core';
import { formatDistanceToNow } from 'date-fns';
import { ptBR } from 'date-fns/locale';

@Pipe({
  name: 'timeAgo',
  standalone: true
})
export class TimeAgoPipe implements PipeTransform {
  transform(value: Date | string | number): string {
    if (!value) return '';
    var date = formatDistanceToNow(new Date(value), { locale: ptBR });
    return date.replace(/^\D+/, '')
  }
}
