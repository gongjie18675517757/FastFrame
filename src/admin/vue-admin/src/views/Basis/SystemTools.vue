<template>
  <v-row align-content="center" justify="center">
    <v-col cols="6">
      <v-card>
        <v-card-text>
          <permission-facatory
            v-for="btn in btns"
            :key="btn.k"
            :permission="`SystemTools.${btn.k}`"
          >
            <v-btn color="primary" block :loading="btn.loading" @click="handleBtnClick(btn)">
              {{ btn.v }}</v-btn
            >
          </permission-facatory>
        </v-card-text>
      </v-card>
    </v-col>
  </v-row>
</template>

<script>
import message from "../../components/Message";
export default {
  data() {
    return {
      btns: Object.entries({
        ReCalcTreeCode: "重算树状码",
      }).map(([k, v]) => ({
        k,
        v,
        loading: false,
      })),
    };
  },
  methods: {
    async handleBtnClick(btn) {
      try {
        btn.loading = true;
        await message.confirm({
          title: "提示",
          content: `确认要执行${btn.v}的操作吗?`,
          timeoute: 5,
        });
        await this.$http.post(`/api/SystemTools/${btn.k}`);
        message.toast.success("执行成功!");
      } finally {
        btn.loading = false;
      }
    },
  },
};
</script>

<style>
</style>