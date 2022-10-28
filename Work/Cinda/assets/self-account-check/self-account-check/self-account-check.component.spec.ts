import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SelfAccountCheckComponent } from './self-account-check.component';

describe('SelfAccountCheckComponent', () => {
  let component: SelfAccountCheckComponent;
  let fixture: ComponentFixture<SelfAccountCheckComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SelfAccountCheckComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SelfAccountCheckComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
