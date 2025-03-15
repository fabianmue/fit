import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { tap } from 'rxjs';

import { ConfirmDialogComponent } from '@app/shared/confirm-dialog/confirm-dialog.component';
import {
  CharacteristicsService,
  HistoricCharacteristicsService,
} from '@app/api/services';
import { CharacteristicReadDto } from '@app/api/models/characteristic-read-dto';
import { CharacteristicCreateDto } from '@app/api/models/characteristic-create-dto';
import { CharacteristicUpdateDto } from '@app/api/models/characteristic-update-dto';
import { HistoricCharacteristicCreateDto } from '@app/api/models/historic-characteristic-create-dto';
import { HistoricCharacteristicReadDto } from '@app/api/models/historic-characteristic-read-dto';
import { HistoricCharacteristicUpdateDto } from '@app/api/models/historic-characteristic-update-dto';
import { CharacteristicType } from '@app/api/models/characteristic-type';
import { CharacteristicComponent } from '@app/characteristics/characteristic/characteristic.component';
import { HistoricCharacteristicComponent } from '@app/characteristics/historic-characteristic/historic-characteristic.component';
import { CharacteristicCreateDialogComponent } from '@app/characteristics/characteristic-create-dialog/characteristic-create-dialog.component';
import { CharacteristicUpdateDialogComponent } from '@app/characteristics/characteristic-update-dialog/characteristic-update-dialog.component';

@Component({
  selector: 'app-characteristics-dashboard',
  imports: [
    MatButtonModule,
    MatIconModule,
    CharacteristicComponent,
    HistoricCharacteristicComponent,
  ],
  templateUrl: './characteristics-dashboard.component.html',
  styleUrl: './characteristics-dashboard.component.scss',
})
export class CharacteristicsDashboardComponent implements OnInit {
  characteristics = new Array<CharacteristicReadDto>();
  historicCharacteristics = new Array<HistoricCharacteristicReadDto>();

  constructor(
    private readonly characteristicsService: CharacteristicsService,
    private readonly historicCharacteristicsService: HistoricCharacteristicsService,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.readCharacteristics();
    this.readHistoricCharacteristics();
  }

  openCreateCharacteristicDialog(): void {
    const dialogRef = this.dialog.open(CharacteristicCreateDialogComponent);

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          historic: boolean | null;
          type: CharacteristicType | null;
          label: string | null;
          color: string | null;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        if (!result.historic) {
          this.createCharacteristic({
            type: result.type,
            label: result.label,
            color: result.color,
          } as CharacteristicCreateDto);
          return;
        }

        this.createHistoricCharacteristic({
          type: result.type,
          label: result.label,
          color: result.color,
        } as HistoricCharacteristicCreateDto);
      }
    );
  }

  openUpdateCharacteristicDialog(characteristic: CharacteristicReadDto): void {
    const dialogRef = this.dialog.open(CharacteristicUpdateDialogComponent, {
      data: {
        type: characteristic.type,
        label: characteristic.label,
        color: characteristic.color,
      },
    });

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          type: CharacteristicType | null;
          label: string | null;
          color: string | null;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.updateCharacteristic(characteristic.id!, {
          type: result.type,
          label: result.label,
          color: result.color,
        } as CharacteristicUpdateDto);
      }
    );
  }

  openUpdateHistoricCharacteristicDialog(
    historicCharacteristic: HistoricCharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(CharacteristicUpdateDialogComponent, {
      data: {
        type: historicCharacteristic.type,
        label: historicCharacteristic.label,
        color: historicCharacteristic.color,
      },
    });

    dialogRef.afterClosed().subscribe(
      (
        result: Partial<{
          type: CharacteristicType | null;
          label: string | null;
          color: string | null;
        }> | null
      ) => {
        if (!result) {
          return;
        }

        this.updateHistoricCharacteristic(historicCharacteristic.id!, {
          type: result.type,
          label: result.label,
          color: result.color,
        } as HistoricCharacteristicUpdateDto);
      }
    );
  }

  openConfirmDeleteCharacteristicDialog(
    characteristic: CharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: `Delete characteristic '${characteristic.label}'?`,
        description: `This will also delete ${characteristic.associatedCompanyCharacteristicCount} associated company characteristics.`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean | null) => {
      if (!result) {
        return;
      }

      this.deleteCharacteristic(characteristic.id!);
    });
  }

  openConfirmDeleteHistoricCharacteristicDialog(
    historicCharacteristic: HistoricCharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: `Delete historic characteristic '${historicCharacteristic.label}'?`,
        description: `This will also delete ${historicCharacteristic.associatedCompanyHistoricCharacteristicCount} associated company historic characteristics.`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((result: boolean | null) => {
      if (!result) {
        return;
      }

      this.deleteHistoricCharacteristic(historicCharacteristic.id!);
    });
  }

  private readCharacteristics(): void {
    this.characteristicsService
      .getCharacteristics()
      .subscribe((characteristics) => (this.characteristics = characteristics));
  }

  private readHistoricCharacteristics(): void {
    this.historicCharacteristicsService
      .getHistoricCharacteristics()
      .subscribe(
        (historicCharacteristics) =>
          (this.historicCharacteristics = historicCharacteristics)
      );
  }

  private createCharacteristic(characteristic: CharacteristicCreateDto): void {
    this.characteristicsService
      .postCharacteristic({ body: characteristic })
      .pipe(tap(() => this.readCharacteristics()))
      .subscribe();
  }

  private createHistoricCharacteristic(
    historicCharacteristic: HistoricCharacteristicCreateDto
  ): void {
    this.historicCharacteristicsService
      .postHistoricCharacteristic({ body: historicCharacteristic })
      .pipe(tap(() => this.readHistoricCharacteristics()))
      .subscribe();
  }

  private updateCharacteristic(
    characteristicId: string,
    characteristic: CharacteristicUpdateDto
  ): void {
    this.characteristicsService
      .putCharacteristic({ id: characteristicId, body: characteristic })
      .pipe(tap(() => this.readCharacteristics()))
      .subscribe();
  }

  private updateHistoricCharacteristic(
    historicCharacteristicId: string,
    historicCharacteristic: HistoricCharacteristicUpdateDto
  ): void {
    this.historicCharacteristicsService
      .putHistoricCharacteristic({
        id: historicCharacteristicId,
        body: historicCharacteristic,
      })
      .pipe(tap(() => this.readHistoricCharacteristics()))
      .subscribe();
  }

  private deleteCharacteristic(characteristicId: string): void {
    this.characteristicsService
      .deleteCharacteristic({ id: characteristicId })
      .pipe(tap(() => this.readCharacteristics()))
      .subscribe();
  }

  private deleteHistoricCharacteristic(historicCharacteristicId: string): void {
    this.historicCharacteristicsService
      .deleteHistoricCharacteristic({ id: historicCharacteristicId })
      .pipe(tap(() => this.readHistoricCharacteristics()))
      .subscribe();
  }
}
