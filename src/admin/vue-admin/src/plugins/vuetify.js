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
    VCheckbox,
    VRow,
    VBtnToggle,
    VInput,
    VAvatar,
    VMenu,
    VListItemAvatar,
    VListItemContent 
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
        VCheckbox,
        VRow,
        VBtnToggle,
        VInput,
        VAvatar,
        VMenu,
        VListItemAvatar,
        VListItemContent
    },

});


export default new Vuetify({
    icons: {
        iconfont: 'mdi'
    },
    lang: {
        locales: { zhHans, },
        current: 'zhHans',
    },
    theme: {
        themes: {
            light: {
                primary: '#1976D2',
                secondary: '#424242',
                accent: '#82B1FF',
                error: '#FF5252',
                info: '#2196F3',
                success: '#4CAF50',
                warning: '#FFC107',
            }
        }
    }
});
