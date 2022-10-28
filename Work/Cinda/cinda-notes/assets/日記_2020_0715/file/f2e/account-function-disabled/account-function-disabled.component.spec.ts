import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccountFunctionDisabledComponent } from './account-function-disabled.component';

describe('AccountFunctionDisabledComponent', () => {
  let component: AccountFunctionDisabledComponent;
  let fixture: ComponentFixture<AccountFunctionDisabledComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AccountFunctionDisabledComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccountFunctionDisabledComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
