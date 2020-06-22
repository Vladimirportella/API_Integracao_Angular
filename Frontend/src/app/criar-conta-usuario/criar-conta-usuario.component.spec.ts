import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CriarContaUsuarioComponent } from './criar-conta-usuario.component';

describe('CriarContaUsuarioComponent', () => {
  let component: CriarContaUsuarioComponent;
  let fixture: ComponentFixture<CriarContaUsuarioComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CriarContaUsuarioComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CriarContaUsuarioComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
