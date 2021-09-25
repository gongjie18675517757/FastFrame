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
    VListItemContent,
    VDivider,
    VSelect,
    VChip
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
        VListItemContent,
        VDivider,
        VSelect,
        VChip
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
        // options: {
        //     themeCache: {
        //         get: key => localStorage.getItem(key),
        //         set: (key, value) => localStorage.setItem(key, value),
        //     },
        // },
        themes: {
            light: {
                p: '#1976D2',
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
