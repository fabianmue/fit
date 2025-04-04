/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { HistoricCharacteristicCreateDto } from '../../models/historic-characteristic-create-dto';
import { HistoricCharacteristicReadDto } from '../../models/historic-characteristic-read-dto';

export interface PostHistoricCharacteristic$Params {
      body?: HistoricCharacteristicCreateDto
}

export function postHistoricCharacteristic(http: HttpClient, rootUrl: string, params?: PostHistoricCharacteristic$Params, context?: HttpContext): Observable<StrictHttpResponse<HistoricCharacteristicReadDto>> {
  const rb = new RequestBuilder(rootUrl, postHistoricCharacteristic.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/json');
  }

  return http.request(
    rb.build({ responseType: 'json', accept: 'application/json', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return r as StrictHttpResponse<HistoricCharacteristicReadDto>;
    })
  );
}

postHistoricCharacteristic.PATH = '/HistoricCharacteristics';
