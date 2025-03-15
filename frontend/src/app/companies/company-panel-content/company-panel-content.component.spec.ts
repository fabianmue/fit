import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CompanyPanelContentComponent } from './company-panel-content.component';

describe('CompanyPanelContentComponent', () => {
  let component: CompanyPanelContentComponent;
  let fixture: ComponentFixture<CompanyPanelContentComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CompanyPanelContentComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(CompanyPanelContentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
