<template>
  <v-card tile elevation="1">
    <v-toolbar dense flat color="transparent" height="30px">
      <v-toolbar-title>流程进度</v-toolbar-title>
      <v-spacer></v-spacer>
      <slot name="fold-button"> </slot>
    </v-toolbar>
    <v-card-text v-show="!isFold">
      <v-timeline dense>
        <v-timeline-item
          v-for="(v, vIndex) in value"
          :key="`${v.Id}_step`"
          :step="vIndex + 1"
          :complete="v.IsFinished"
          fill-dot
          :color="v.IsFinished?'primary':null"
          :large="v.IsFinished"
          :small="!v.IsFinished"
        >
          <template v-slot:icon v-if="v.IsFinished">
            <v-icon color="#fff" v-if="['submit','pass'].includes(v.Action)">check</v-icon>
            <v-icon color="#fff" v-else-if="['ng'].includes(v.Action)">clear</v-icon>
            <v-icon color="#fff" v-else-if="['unsubmit','uncheck'].includes(v.Action)">mdi-restore</v-icon>
          </template>
          <strong>{{ v.FlowNodeName }}</strong>
          <div v-if="v.IsFinished">
            {{ v.OperateTime }} {{ v.OperaterName }}: {{ v.Desc }}
          </div>
          <div v-else style="height: auto">
            待审核 可审核人:
            <v-chip small label v-for="u in v.Checker" :key="u.Key" style="margin-left:5px;">
              {{ u.Value }}
            </v-chip>
          </div>
        </v-timeline-item>
      </v-timeline>
    </v-card-text>
  </v-card>

  <!-- <v-stepper-content :key="`${v.Id}_content`" :step="vIndex + 1">
            
          </v-stepper-content> -->
</template>

<script>
export default {
  props: {
    value: Array,
    isFold: Boolean,
  },
};
</script>

<style>
</style>