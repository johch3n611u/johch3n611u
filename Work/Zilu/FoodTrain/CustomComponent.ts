import { Component, OnDestroy, OnInit } from '@angular/core';
import { E2HelpFaqCMSComponent } from '@elab/e2-lib/core';
import { CmsService } from '@spartacus/core';
import { CmsComponentData } from '@spartacus/storefront';
import { BehaviorSubject, Observable } from 'rxjs';
import { map, switchMap, take, tap } from 'rxjs/operators';

@Component({
  selector: 'pns-help-faq',
  templateUrl: './pns-help-faq.component.html',
  html:
  
  <ng-container *ngIf="component$ | async as data">
    <div class="faq-item" (click)="isActive$.next(!isActive$.value)">
        <div class="faq-context">{{ data.question }}</div>
        <img src="assets/img/pns-help-faq-arrow.svg" [ngClass]="(isActive$ | async) ? 'arrow-up' : 'arrow-down'"/>
    </div>

    <div class="faq-response" [class.active]="isActive$ | async" *ngIf="isActive$ | async">
      <div class="content" [innerHTML]="data.answer"></div>
      <div class="logo"><img alt="" src="assets/img/share/food_angel_img.png" /></div>
    </div>
  </ng-container>
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


---
  
## component$ => result 5 次的

{
    "uid": "FoodAngelFaq1",
    "uuid": "eyJpdGVtSWQiOiJGb29kQW5nZWxGYXExIiwiY2F0YWxvZ0lkIjoicG5zaGtDb250ZW50Q2F0YWxvZyIsImNhdGFsb2dWZXJzaW9uIjoiT25saW5lIn0=",
    "typeCode": "E2HelpFaqComponent",
    "name": "Food Angel FAQ Q1",
    "container": "false",
    "answer": "A: 百佳超級市場經已第8年聯同「惜食堂」展開「全城傳愛齊捐食」食物捐贈計劃，呼籲全港市民身體力行關注弱勢社群。配合政府電子消費券計劃，百佳將於旗下全線香港分店首次推出「消費『餸』愛心」食物捐贈計劃，希望鼓勵大家用消費券購物之餘亦可以透過捐贈愛心食材為善助人。連同百佳及家樂牌第3度推出的1%對捐活動，希望於疫情下為惜食堂籌集更多食材製作營養飯餐予有需要人士。顧客在過去8屆活動期間共連同百佳配對捐贈的「佳之選」貨品及品牌對捐產品，已籌集超過460,000件糧油食物予惜食堂，食物和捐款總值已超過港幣1,000萬！百佳會把籌集所得食物運往「惜食堂」以製作飯餐，預計可助「惜食堂」供應數十萬個熱飯餐，免費派發予社會上有需要人士。",
    "question": "Q:「全城傳愛齊捐食消費愛心」是什麼樣的活動?",
    "synchronizationBlocked": "false",
    "ajax": "false",
    "modifiedTime": "2022-11-24T11:14:04.109+08:00"
}
