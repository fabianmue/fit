/* tslint:disable */
/* eslint-disable */
/* Code generated by ng-openapi-gen DO NOT EDIT. */

import { HttpClient, HttpContext, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { StrictHttpResponse } from '../../strict-http-response';
import { RequestBuilder } from '../../request-builder';

import { CompanyHistoricCharacteristicCreateDto } from '../../models/company-historic-characteristic-create-dto';

export interface PostCompanyHistoricCharacteristic$Params {
      body?: CompanyHistoricCharacteristicCreateDto
}

export function postCompanyHistoricCharacteristic(http: HttpClient, rootUrl: string, params?: PostCompanyHistoricCharacteristic$Params, context?: HttpContext): Observable<StrictHttpResponse<void>> {
  const rb = new RequestBuilder(rootUrl, postCompanyHistoricCharacteristic.PATH, 'post');
  if (params) {
    rb.body(params.body, 'application/json');
  }

  return http.request(
    rb.build({ responseType: 'text', accept: '*/*', context })
  ).pipe(
    filter((r: any): r is HttpResponse<any> => r instanceof HttpResponse),
    map((r: HttpResponse<any>) => {
      return (r as HttpResponse<any>).clone({ body: undefined }) as StrictHttpResponse<void>;
    })
  );
}

postCompanyHistoricCharacteristic.PATH = '/CompanyHistoricCharacteristics';
