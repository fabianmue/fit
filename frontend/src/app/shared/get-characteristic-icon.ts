import { CharacteristicReadDto } from '@app/api/models/characteristic-read-dto';
import { CompanyCharacteristicReadDto } from '@app/api/models/company-characteristic-read-dto';
import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';
import { HistoricCharacteristicReadDto } from '@app/api/models/historic-characteristic-read-dto';

export function getCharacteristicIcon(
  characteristic:
    | CharacteristicReadDto
    | HistoricCharacteristicReadDto
    | CompanyCharacteristicReadDto
    | CompanyHistoricCharacteristicReadDto
) {
  switch (characteristic.type) {
    case 'Financial':
      return 'paid';
    default:
      return 'account_balance';
  }
}
