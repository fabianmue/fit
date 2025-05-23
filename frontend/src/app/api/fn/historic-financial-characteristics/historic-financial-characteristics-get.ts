/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { HistoricFinancialCharacteristicReadDto } from '../../models/historic-financial-characteristic-read-dto';

export interface HistoricFinancialCharacteristicsGet$Params {
}

export function historicFinancialCharacteristicsGet(http: HttpClient, rootUrl: string, params?: HistoricFinancialCharacteristicsGet$Params, context?: HttpContext): Observable<StrictHttpResponse<Array<HistoricFinancialCharacteristicReadDto>>> {
  const rb = new RequestBuilder(rootUrl, historicFinancialCharacteristicsGet.PATH, 'get');
  if (params) {
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'application/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<Array<HistoricFinancialCharacteristicReadDto>>;
    })
  );
}

historicFinancialCharacteristicsGet.PATH = '/HistoricFinancialCharacteristics';
