<template>
  <v-menu top offset-y :close-on-content-click="false" v-model="verifying">
    <template v-slot:activator="props"> 
      <slot name="activator" v-bind="props"></slot>
    </template>
    <v-card tile v-if="model && verifying" :style="{ width: `${model.BgWidth + 30}px` }">
      <v-toolbar flat dense>
        <v-toolbar-title>滑动验证</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-btn icon @click="refresh">
          <v-icon>refresh</v-icon>
        </v-btn>
      </v-toolbar>
      <v-divider></v-divider>
      <v-card-text>
        <div class="silde-container">
          <img :src="model.BackgroundImg" class="bg-img" alt="" srcset="" />
          <img
            :src="model.SlideImg"
            class="slide-img"
            :style="{ top: `${y}px`, left: `${x}px` }"
            alt=""
            srcset=""
          />
          <div class="silde-bar" :style="{ width: `${model.BgWidth}px` }">
            <div
              class="silde-bar-indicator"
              :style="{
                width: `${x}px`,
                background:
                  verifyResult == -1
                    ? '#FCE1E1'
                    : verifyResult == 1
                    ? '#47C0C7'
                    : '',
                'border-color':
                  verifyResult == -1
                    ? '#f57a7a'
                    : verifyResult == -1
                    ? '#47C0C7'
                    : '',
              }"
            ></div>
            <div
              class="silde-bar-control"
              :style="{
                left: `${x}px`,
                background:
                  verifyResult == -1
                    ? '#E67C83'
                    : verifyResult == 1
                    ? '#47C0C7'
                    : '',
              }"
              @mousedown="handleMouseDown"
            >
              <v-icon size="25" v-if="verifyResult == -1">mdi-close</v-icon>
              <v-icon size="25" v-else-if="verifyResult == 1">mdi-check</v-icon>
              <v-icon
                size="25"
                v-else
                :style="{ background: verifyResult == 1 ? '#fff' : '' }"
                >mdi-arrow-right</v-icon
              >
            </div>
            <div class="silde-bar-tips">
              {{ moveing ? "" : "向右拖动滑块填充拼图" }}
            </div>
          </div>
        </div>
      </v-card-text>
    </v-card>
  </v-menu>
</template>

<script>
export default {
  data() {
    return {
      verifying:false,
      model: null,
      y: null,
      x: 0,
      moveing: false,
      verifyResult: null, //null:未验证 1:成功 -1:失败
    };
  },
  mounted() {
    this.refresh();
  },
  methods: {
    refresh() {
      this.$http.get(`/api/Account/SlideVerifify`).then((res) => {
        this.model = res;
        this.y = res.PositionY;
        this.x = 0;
        this.verifyResult = null;
      });
    },
    handleMouseDown() {
      this.moveing = true;
      window.addEventListener("mousemove", this.handleMouseMove);
      window.addEventListener("mouseup", this.handleMouseUp);
    },
    handleMouseMove(e) {
      let x = this.x + e.movementX;

      if (x < 0) x = 0;
      if (x > this.model.BgWidth - this.model.SlideWidth)
        x = this.model.BgWidth - this.model.SlideWidth;

      this.x = x;
    },
    handleMouseUp() {
      window.removeEventListener("mousemove", this.handleMouseMove);
      window.removeEventListener("mouseup", this.handleMouseUp);
      this.handleVerify(this.x);
    },
    handleVerify(x) {
      this.$http.post(`/api/Account/IsVerify`, `positionX=${x}`).then((res) => {
        if (res) {
          this.verifyResult = 1;
          this.$emit("success");
        } else {
          this.verifyResult = -1;
          setTimeout(() => {
            this.verifyResult = null;
            this.moveing = false;
            this.x = 0;
            this.refresh();
          }, 1.2 * 1000);
        }
      });
    },
  },
};
</script>

<style lang="stylus" scoped>
.silde-container {
  position: relative;

  img {
    user-select: none;
  }

  .slide-img {
    position: absolute;
  }

  .silde-bar {
    width: 100%;
    border: 1px solid #e4e7eb;
    background-color: #f7f9fa;
    height: 40px;
    border-radius: 2px;
    box-sizing: border-box;
    position: relative;

    .silde-bar-indicator {
      border-radius: 2px;
      position: absolute;
      border: 1px solid #1991fa;
      height: 38px;
      background: #D1E9FE;
    }

    .silde-bar-tips {
      border-radius: 2px;
      position: absolute;
      line-height: 40px;
      padding-left: 40px;
      text-align: center;
      width: 100%;
      user-select: none;
    }

    .silde-bar-control {
      text-align: center;
      border-radius: 2px;
      width: 40px;
      position: absolute;
      background-color: #fff;
      box-shadow: 0 0 3px rgb(0 0 0 / 30%);
      cursor: pointer;
      transition: background 0.2s linear;
      line-height: 38px;
      height: 38px;
      z-index: 9;
    }

    .silde-bar-control:hover {
      background-color: #1991fa;

      i {
        color: #fff;
      }
    }
  }
}
</style>

 