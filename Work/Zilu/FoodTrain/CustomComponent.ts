import { Component, OnDestroy, OnInit } from '@angular/core';
import { E2HelpFaqCMSComponent } from '@elab/e2-lib/core';
import { CmsService } from '@spartacus/core';
import { CmsComponentData } from '@spartacus/storefront';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, switchMap, take, tap } from 'rxjs/operators';

@Component({
  selector: 'pns-help-faq',
  templateUrl: './pns-help-faq.component.html',
})
export class PnsHelpFaqComponent implements OnInit, OnDestroy {
  component$: Observable<E2HelpFaqCMSComponent> = this.cmsService
    .getContentSlot('FoodAngelFaq')
    .pipe(
      map(p => p.components),
      map(components =>
        components.filter(item => item.typeCode === 'E2HelpFaqComponent')
      ),
      switchMap(components =>
        this.componentData.data$.pipe(
          take(1),
          tap(data => {
            if (data && components.length > 0) {
              this.isActive$.next(data.uid === components[0].uid);
            }
          })
        )
      )
    );

  isActive$ = new BehaviorSubject(false);

  constructor(
    private cmsService: CmsService,
    public componentData: CmsComponentData<E2HelpFaqCMSComponent>
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.isActive$?.unsubscribe();
  }
}
