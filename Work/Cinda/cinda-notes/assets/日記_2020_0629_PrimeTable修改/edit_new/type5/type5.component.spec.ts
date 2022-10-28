import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Type5Component } from './type5.component';

describe('Type5Component', () => {
  let component: Type5Component;
  let fixture: ComponentFixture<Type5Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Type5Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Type5Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
