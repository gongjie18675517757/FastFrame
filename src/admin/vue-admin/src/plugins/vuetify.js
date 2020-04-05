import Vue from 'vue';
import Vuetify, {
    VLayout,
    VFlex,
    VSpacer,
    VTextField,
    VCard,
    VCardText,
    VToolbar,
    VToolbarTitle,
    VBtn,
    VDialog,
    VIcon,
    VAutocomplete,
    VList,
    VListItem,
    VListItemTitle,
    VTextarea,
    VContainer,
    VImg,
    VCheckbox
} from 'vuetify/lib';
import zhHans from 'vuetify/es5/locale/zh-Hans'

Vue.use(Vuetify, {
    components: {
        VLayout,
        VFlex,
        VSpacer,
        VTextField,
        VCard,
        VCardText,
        VToolbar,
        VToolbarTitle,
        VBtn,
        VDialog,
        VIcon,
        VAutocomplete,
        VList,
        VListItem,
        VListItemTitle,
        VTextarea,
        VContainer,
        VImg,
        VCheckbox
    }
});

export default new Vuetify({
    icons: {
        iconfont: 'mdi',
    },
    lang: {
        locales: { zhHans, },
        current: 'zhHans',
    },

});
