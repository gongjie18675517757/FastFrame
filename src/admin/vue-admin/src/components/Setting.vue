<template>
  <v-navigation-drawer
    temporary
    right
    v-model="rightDrawer"
    fixed
    hide-overlay
    app
  >
    <div id="appDrawer">
      <v-toolbar color="primary darken-1" dark dense>
        <v-toolbar-title>设置</v-toolbar-title>
      </v-toolbar>
      <vue-perfect-scrollbar
        class="drawer-menu--scroll"
        :settings="scrollSettings"
      >
        <v-container>
          <v-layout column>
            <v-flex>
              <div class="theme-options">
                <v-subheader class="px-1 my-2">页面模式</v-subheader>
                <v-divider></v-divider>
                <div class="my-3">
                  <v-btn-toggle v-model="singlePageMode">
                    <v-btn text :value="true">单页</v-btn>
                    <v-btn text :value="false">多页签</v-btn>
                  </v-btn-toggle>
                </div>
              </div>
              <div class="theme-options" v-if="!$vuetify.breakpoint.smAndDown">
                <v-subheader class="px-1 my-2">表单展现模式</v-subheader>
                <v-divider></v-divider>
                <div class="my-3">
                  <v-btn-toggle v-model="dialogMode">
                    <v-btn text :value="true">弹窗模式</v-btn>
                    <v-btn text :value="false">页面模式</v-btn>
                  </v-btn-toggle>
                </div>
              </div>
              <div class="theme-options">
                <v-subheader class="px-1 my-2">全局主题</v-subheader>
                <v-divider></v-divider>
                <div class="my-3">
                  <v-btn-toggle v-model="sideBarOption">
                    <v-btn text value="dark">昏暗</v-btn>
                    <v-btn text value="light">明亮</v-btn>
                  </v-btn-toggle>
                </div>
              </div>
              <v-subheader class="px-1 my-2">主题颜色</v-subheader>
              <v-divider></v-divider>
              <div class="color-option">
                <v-layout wrap>
                  <label
                    class="color-option--label flex xs6 pa-1"
                    v-for="(option, index) in themeColorOptions"
                    :key="index"
                  >
                    <input
                      type="radio"
                      name="color"
                      v-bind:value="option.key"
                      v-model="themeColor"
                    />
                    <span class="color-option--item bg">
                      <span class="overlay">
                        <span class="material-icons">check</span>
                      </span>
                      <span
                        class="color-option--item--header sideNav"
                        :class="option.value.sideNav"
                      ></span>
                      <span
                        class="color-option--item--header mainNav"
                        :class="option.value.mainNav"
                      ></span>
                      <span
                        class="sideMenu"
                        :class="option.value.sideManu"
                      ></span>
                    </span>
                  </label>
                </v-layout>
              </div>
            </v-flex>
          </v-layout>
        </v-container>
      </vue-perfect-scrollbar>
    </div>
  </v-navigation-drawer>
</template>

<script>
import colors from "vuetify/es5/util/colors";
import VuePerfectScrollbar from "vue-perfect-scrollbar";

export default {
  components: {
    VuePerfectScrollbar,
  },
  props: {},
  data() {
    return {
      show: false,
      sideBarOption: "light",
      colors: colors,
      // themeColor: "indigo",
      scrollSettings: {
        maxScrollbarLength: 160,
      },
      themeColorOptions: [
        {
          key: "blue",
          value: {
            sideNav: "blue",
            mainNav: "blue",
            sideManu: "white",
          },
        },
        {
          key: "teal",
          value: {
            sideNav: "teal",
            mainNav: "teal",
            sideManu: "white",
          },
        },
        {
          key: "red",
          value: {
            sideNav: "red",
            mainNav: "red",
            sideManu: "white",
          },
        },
        {
          key: "orange",
          value: {
            sideNav: "orange",
            mainNav: "orange",
            sideManu: "white",
          },
        },
        {
          key: "purple",
          value: {
            sideNav: "purple",
            mainNav: "purple",
            sideManu: "white",
          },
        },
        {
          key: "indigo",
          value: {
            sideNav: "indigo",
            mainNav: "indigo",
            sideManu: "white",
          },
        },
        {
          key: "cyan",
          value: {
            sideNav: "cyan",
            mainNav: "cyan",
            sideManu: "white",
          },
        },
        {
          key: "pink",
          value: {
            sideNav: "pink",
            mainNav: "pink",
            sideManu: "white",
          },
        },
        {
          key: "green",
          value: {
            sideNav: "green",
            mainNav: "green",
            sideManu: "white",
          },
        },
      ],
    };
  },
  computed: {
    themeColor: {
      get() {
        return this.$store.state.themeColor;
      },
      set(val) {
        this.$store.commit({
          type: "setThemeColor",
          value: val,
        });
      },
    },
    rightDrawer: {
      get() {
        return this.$store.state.rightDrawer;
      },
      set(val) {
        this.$store.commit({
          type: "toggleRightDrawer",
          value: val,
        });
      },
    },
    singlePageMode: {
      get() {
        return this.$store.state.singlePageMode;
      },
      set() {
        this.$store.commit({
          type: "togglePageMode",
        });
      },
    },
    dialogMode: {
      get() {
        return this.$store.state.dialogMode;
      },
      set() {
        this.$store.commit({
          type: "toggleDialogMode",
        });
      },
    },
  },
  watch: {
    themeColor(val) {
      this.$vuetify.theme.themes.light.primary = this.colors[val].base;
      this.$vuetify.theme.themes.dark.primary = this.colors[val].base;
    },
    sideBarOption(val) {
      this.$vuetify.theme.dark = val === "dark";
    },
  },
};
</script>

<style lang="stylus" scoped>
.color-option {
  &--label {
    position: relative;
    display: block;
    cursor: pointer;

    & input[type='radio'] {
      display: none;

      &+span {
        position: relative;

        &>.overlay {
          display: none;
          position: absolute;
          top: 0;
          bottom: 0;
          right: 0;
          left: 0;
          width: 100%;
          height: 100%;
          background-color: rgba(0, 0, 0, 0.3);
          text-align: center;
          line-height: 30px;
          color: #fff;
        }
      }

      &:checked+span>.overlay {
        display: block;
      }
    }

    & .bg {
      background-color: #f1f1f1;
    }
  }

  &--item {
    overflow: hidden;
    display: block;
    box-shadow: 0 0 2px rgba(0, 0, 0, 0.1);
    margin-bottom: 15px;

    &--header {
      height: 10px;
    }

    &>span {
      display: block;
      float: left;
      width: 50%;
      height: 20px;
    }
  }
}

#appDrawer {
  overflow: hidden;

  .drawer-menu--scroll {
    height: calc(100vh - 48px);
    overflow: auto;
  }
}
</style>

