import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DeviceReturnComponent } from './device-return.component';

describe('DeviceReturnComponent', () => {
  let component: DeviceReturnComponent;
  let fixture: ComponentFixture<DeviceReturnComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DeviceReturnComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DeviceReturnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
