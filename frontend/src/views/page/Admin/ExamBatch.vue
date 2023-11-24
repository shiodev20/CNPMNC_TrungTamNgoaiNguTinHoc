<template>
  <div class="exam_parent">
    <baselist v-if="this.$route.query.action == null"></baselist>
    <baseadd
      v-if="
        this.$route.query.action == 'create' ||
        this.$route.query.action == 'edit'
      "
    ></baseadd>
    <basedetail v-if="this.$route.query.action == 'detail-cus'"></basedetail>
    <baseclipboard
      v-if="this.$route.query.action == 'clipboard-cus'"
    ></baseclipboard>
  </div>
</template>
<script>
import add from "@/components/exambatch/Add_Exambatch.vue";
import list from "@/components/exambatch/List_Exambatch.vue";
import detail from "@/components/exambatch/Detail_Exambatch.vue";
import clipboard from "@/components/exambatch/ClipBoard.vue";
export default {
  name: "ExamV",
  components: {
    baseadd: add,
    baselist: list,
    basedetail: detail,
    baseclipboard: clipboard,
  },
};
</script>
<style scoped></style>
