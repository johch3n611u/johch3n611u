import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ConfigModule } from '@spartacus/core';
import { PnsHelpFaqComponent } from './pns-help-faq.component';

@NgModule({
  declarations: [PnsHelpFaqComponent],
  imports: [
    CommonModule, // @angular/common 基礎的 Angular 指令和管道， 例如 NgIf、NgForOf 由 Angular Core 提供
    ConfigModule.withConfig({ // @Spartacus/core 模組
      cmsComponents: { // 較複雜的 cmsComponent 要配合 ts 檔案內的語法去置換 Component 可參考 https://www.youtube.com/watch?v=3xhnYUK9-sc&ab_channel=Divante
        E2HelpFaqComponent: {
          component: PnsHelpFaqComponent,
        },
      },
    }),
  ],
})
export class PnsHelpFaqModule {}
