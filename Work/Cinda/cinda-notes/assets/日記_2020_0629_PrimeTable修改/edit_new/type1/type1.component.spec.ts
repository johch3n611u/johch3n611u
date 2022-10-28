import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { Type1Component } from './type1.component';

describe('Type1Component', () => {
  let component: Type1Component;
  let fixture: ComponentFixture<Type1Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ Type1Component ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Type1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
