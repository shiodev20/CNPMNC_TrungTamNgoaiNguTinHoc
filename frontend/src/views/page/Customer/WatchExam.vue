<template>
  <div class="parnet">
    <baselist v-if="this.$route.query.action != 'registed'"></baselist>
    <baseexambatch
      v-if="this.$route.query.action == 'registed'"
    ></baseexambatch>
  </div>
</template>
<script>
import list from "@/components/customer/Exam/List_Watch_exam.vue";
import exambatch from "@/components/customer/Exam/List_Watch_Exambatch.vue";
export default {
  components: {
    baselist: list,
    baseexambatch: exambatch,
  },
  setup() {},
};
</script>
<style scoped>
.parnet {
  position: absolute;
  inset: 0;
}
</style>
