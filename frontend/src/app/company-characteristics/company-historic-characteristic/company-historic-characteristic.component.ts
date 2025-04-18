import { AsyncPipe } from '@angular/common';
import {
  Component,
  EventEmitter,
  Input,
  Output,
  AfterViewInit,
  ViewChild,
  ElementRef,
  OnChanges,
  SimpleChanges,
} from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { Chart } from 'chart.js/auto';
import { Observable } from 'rxjs';

import { CompanyHistoricCharacteristicReadDto } from '@app/api/models/company-historic-characteristic-read-dto';
import { configureCompanyHistoricCharacteristicScatterChart } from '@app/company-characteristics/historic-values-scatter-chart';
import { AuthenticationService } from '@app/shared/authentication.service';
import { getCharacteristicIcon } from '@app/shared/get-characteristic-icon';

@Component({
  selector: 'app-company-historic-characteristic',
  imports: [
    AsyncPipe,
    MatButtonModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    ReactiveFormsModule,
  ],
  templateUrl: './company-historic-characteristic.component.html',
  styleUrl: './company-historic-characteristic.component.scss',
})
export class CompanyHistoricCharacteristicComponent
  implements OnChanges, AfterViewInit
{
  @Input({ required: true })
  companyHistoricCharacteristic!: CompanyHistoricCharacteristicReadDto;
  @Output() update = new EventEmitter<void>();
  @Output() delete = new EventEmitter<void>();
  @ViewChild('chartCanvas')
  private chartCanvas?: ElementRef<HTMLCanvasElement>;
  authenticated$: Observable<boolean>;
  private chart?: Chart<'scatter'>;

  get icon(): string {
    return getCharacteristicIcon(this.companyHistoricCharacteristic);
  }

  constructor(private readonly authenticationService: AuthenticationService) {
    this.authenticated$ = this.authenticationService.authenticated$;
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['companyHistoricCharacteristic']) {
      this.refreshChart();
    }
  }

  ngAfterViewInit(): void {
    this.refreshChart();
  }

  onUpdate(): void {
    this.update.emit();
  }

  onDelete(): void {
    this.delete.emit();
  }

  private refreshChart(): void {
    if (!this.chartCanvas) {
      return;
    }

    this.chart?.destroy();
    this.chart = configureCompanyHistoricCharacteristicScatterChart(
      this.chartCanvas.nativeElement,
      this.companyHistoricCharacteristic
    );
  }
}
