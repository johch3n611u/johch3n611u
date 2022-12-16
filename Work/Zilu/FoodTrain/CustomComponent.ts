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
      map(p => p.components), // map 將一個訂閱可以得到的資料轉換成另外一筆資料
      map(components =>
        components.filter(item => item.typeCode === 'E2HelpFaqComponent')
      ),
      switchMap(components => // switchMap 收到 observable 資料時，轉換成另外一個 observable
        this.componentData.data$.pipe(
          take(1), // 取开头n个值
          tap(data => { // tap 什麼都不影響可以在這做某些動作
            if (data && components.length > 0) {
              this.isActive$.next(data.uid === components[0].uid);
            }
          })
        )
      )
    );

  isActive$ = new BehaviorSubject(false); // 可以有初始值，一註冊馬上拿到值 https://blog.kevinyang.net/2017/02/26/rxjs-context-demo-3/

  constructor(
    private cmsService: CmsService,
    public componentData: CmsComponentData<E2HelpFaqCMSComponent>
  ) {}

  ngOnInit(): void {}

  ngOnDestroy(): void {
    this.isActive$?.unsubscribe();
  }
}
