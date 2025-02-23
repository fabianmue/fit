import { Component, OnInit } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { tap } from 'rxjs';

import { CharacteristicComponent } from '../characteristic/characteristic.component';
import { CharacteristicCreateDialogComponent } from '../characteristic-create-dialog/characteristic-create-dialog.component';
import { CharacteristicUpdateDialogComponent } from '../characteristic-update-dialog/characteristic-update-dialog.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { CharacteristicsService } from '../api/services';
import { CharacteristicReadDto } from '../api/models/characteristic-read-dto';
import { CharacteristicCreateDto } from '../api/models/characteristic-create-dto';
import { CharacteristicUpdateDto } from '../api/models/characteristic-update-dto';

@Component({
  selector: 'app-characteristics-dashboard',
  imports: [MatButtonModule, MatIconModule, CharacteristicComponent],
  templateUrl: './characteristics-dashboard.component.html',
  styleUrl: './characteristics-dashboard.component.scss',
})
export class CharacteristicsDashboardComponent implements OnInit {
  characteristics = new Array<CharacteristicReadDto>();

  constructor(
    private readonly characteristicsService: CharacteristicsService,
    private readonly dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.readCharacteristics();
  }

  openCreateCharacteristicDialog(): void {
    const dialogRef = this.dialog.open(CharacteristicCreateDialogComponent, {
      width: '600px',
    });

    dialogRef
      .afterClosed()
      .subscribe((dialogResult: CharacteristicCreateDto | null) => {
        if (dialogResult) {
          this.createCharacteristic(dialogResult);
        }
      });
  }

  openUpdateCharacteristicDialog(characteristic: CharacteristicReadDto): void {
    const dialogRef = this.dialog.open(CharacteristicUpdateDialogComponent, {
      width: '600px',
      data: { characteristic },
    });

    dialogRef
      .afterClosed()
      .subscribe((dialogResult: CharacteristicUpdateDto | null) => {
        if (dialogResult) {
          this.updateCharacteristic(characteristic.id!, dialogResult);
        }
      });
  }

  openConfirmDeleteCharacteristicDialog(
    characteristic: CharacteristicReadDto
  ): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '600px',
      data: {
        title: `Delete characteristic '${characteristic.label}' ?`,
        description: `This will also delete ${characteristic.associatedCompanyCharacteristicCount} associated company characteristics.`,
        confirmLabel: 'Delete',
      },
    });

    dialogRef.afterClosed().subscribe((dialogResult: boolean | null) => {
      if (dialogResult) {
        this.deleteCharacteristic(characteristic.id!);
      }
    });
  }

  private readCharacteristics(): void {
    this.characteristicsService
      .characteristicsGet()
      .subscribe((characteristics) => (this.characteristics = characteristics));
  }

  private createCharacteristic(characteristic: CharacteristicCreateDto): void {
    this.characteristicsService
      .characteristicsPost({ body: characteristic })
      .pipe(tap(() => this.readCharacteristics()))
      .subscribe();
  }

  private updateCharacteristic(
    characteristicId: string,
    characteristic: CharacteristicUpdateDto
  ): void {
    this.characteristicsService
      .characteristicsIdPut({ id: characteristicId, body: characteristic })
      .pipe(tap(() => this.readCharacteristics()))
      .subscribe();
  }

  private deleteCharacteristic(characteristicId: string): void {
    this.characteristicsService
      .characteristicsIdDelete({ id: characteristicId })
      .pipe(tap(() => this.readCharacteristics()))
      .subscribe();
  }
}
